using System.Text;
using DungeonsAndDragons.Game.Common;
using DungeonsAndDragons.Shared;
using DungeonsAndDragons.Shared.Models;
using Microsoft.Extensions.Primitives;

namespace DungeonsAndDragons.Server.Services;

public class HonestFightSimulator : IFightSimulator
{
    private readonly ILogger<HonestFightSimulator> _logger;

    public HonestFightSimulator(ILogger<HonestFightSimulator> logger)
    {
        _logger = logger;
    }
    public async Task<FightEndDTO> SimulateFightAsync(FightStartDTO dto)
    {
        await Task.Yield();
        var dice = new GameDice(20, 1);
        var player = dto.Player;
        var monster = dto.Monster;
        var round = 1;
        var logs = new List<RoundLog>();
        while (IsAlive(player) && IsAlive(monster))
        {
            _logger.LogInformation("Round: {RoundNumber}", round);
            var log = MakeRound(player, monster, dice);
            _logger.LogInformation("Player: {PlayerResult}\nMonster: {MonsterResult}", log.PlayerStatus, log.MonsterStatus);
            log.RoundNumber = round;
            round++;
            logs.Add(log);
        }
        return new FightEndDTO()
               {
                   Logs = logs,
                   UserEndStatus = player
               };
    }

    private static int GetProbabilityModifier(Entity entity)
    {
        return entity.AttackModifier;
    }
    
    
    private static RoundLog MakeRound(Entity player, Entity monster, GameDice probabilityDice)
    {
        var log = new RoundLog();
        log.PlayerStatus = log.MonsterStatus = string.Empty;
        var playerDice = new GameDice(player.DamageMax, player.DamageCount);
        var playerResult = SimulateFightBetween(player, monster, probabilityDice, playerDice);
        log.PlayerStatus = playerResult;
        
        if (!IsAlive(monster))
        {
            log.PlayerStatus += " You win.";
            return log;
        }

        var monsterDice = new GameDice(monster.DamageMax, monster.DamageCount);
        var monsterResult = SimulateFightBetween(monster, player, probabilityDice, monsterDice);
        if (!IsAlive(player))
        {
            monsterResult += $". {monster.Name} wins";
        }
        log.MonsterStatus = monsterResult;
        return log;
    }

    private static string SimulateFightBetween(Entity first,
                                               Entity second,
                                               GameDice probabilityDice,
                                               GameDice firstEntityDamageDice)
    {
        var builder = new StringBuilder();
        var probability = probabilityDice.Roll();
        builder.Append(first.FormatProbability(probability));
        if (probability == 1)
        {
            builder.Append(" critical miss.");
            return builder.ToString();
        }

        probability += first.AttackModifier;
        if (probability < second.ArmorClass)
        {
            builder.Append(" lesser than ");
            builder.Append(second.ArmorClass);
            builder.Append(", miss.");
            return builder.ToString();
        }

        builder.Append(" greater than ");
        builder.Append(second.ArmorClass);
        builder.Append(probability == 20
                           ? ", critical hit. "
                           : ", hit. ");

        var damage = firstEntityDamageDice.Roll();
        builder.Append(damage);
        var modifier = first.AttackModifier;
        builder.Append($" (+{modifier})");

        var totalDamage = ( damage + modifier );
        
        if (probability == 20)
        {
            builder.Append(" * 2 ");
            totalDamage *= 2;
        }
        

        builder.Append(" hits enemy by ");
        builder.Append(totalDamage);
        second.HitPoints -= totalDamage;
        builder.Append($"({second.HitPoints})");

        if (second.HitPoints <= 0)
        {
            builder.Append(second.Name);
            builder.Append(" is dead. ");
        }

        return builder.ToString();
    }

    private static bool IsAlive(Entity entity) => entity.HitPoints > 0;
}
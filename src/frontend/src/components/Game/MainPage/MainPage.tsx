import React, {FC, useEffect, useState} from 'react';
import MainPageProps from "./MainPageProps";
import {GameStatus} from "../../../models/gameStatus";
import Game from "../../../models/game";

const MainPage: FC<MainPageProps> = ({onGameStarted: onGameStartedParent, 
                                         gamesRepository, 
                                         gameCommunicator}) => {
    const [allGames, setAllGames] = useState<Game[]>([]);
    useEffect(() => {
        gamesRepository.getGamesPagedAsync(1, 20)
            .then(games => setAllGames(games))
            .catch(e => alert(e));
        gameCommunicator.registerOnGameStartedCallback(onGameStarted);
        return () => {
            gameCommunicator.unregisterOnGameStartedCallback(onGameStarted);
        }
    }, [gamesRepository])

    const [freeze, setFreeze] = useState(false);


    function onGameStarted(game: Game) {
        setFreeze(false);
        onGameStartedParent(game);
    }

    async function onGameStartClick(gameId: string) {
        if (freeze || !gameId) {
            return;
        }


        setFreeze(true);
        let success = false;
        try {
            success = await gameCommunicator.connectToGameAsync(gameId);
        } finally {
            if (!success) {
                setFreeze(false);
            }
        }
    }
    
    
    async function createGame() {
        if (freeze) {
            return;
        }
        setFreeze(true);
        await gamesRepository.createGameAsync(rank)
            .catch(() => setFreeze(false));
    }
    
    const [rank, setRank] = useState(0);
    
    return (
        <div>
            <form>
                <label>
                    Максимальный ранг
                    <input type={'number'} value={rank} onChange={x => setRank(Number.parseInt(x.currentTarget.value))}/>
                </label>
                <button type={'button'} onClick={createGame}>Создать игру</button>
            </form>
            {
                allGames.map(g => (
                    <li>
                        {g.id} - {g.status}
                        {
                            g.status === GameStatus.Created
                            && <button onClick={async () => await onGameStartClick(g.id)}>
                                Присоединиться
                            </button>
                        }
                    </li>
                ))
            }
        </div>
    );
};

export default MainPage;

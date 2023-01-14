import React, {FC, useState} from 'react';
import GameProps from "./GameProps";
import RealGamePage from "./RealGame/RealGamePage";
import MainPage from "./MainPage/MainPage";
import GameResult from "../../models/gameResult";
import Game from "../../models/game";

export const GamePage: FC<GameProps> = ({gamesRepository, gameCommunicator}) => {
    const [game, setGame] = useState<Game>();
    const [runGame, setRunGame] = useState(false);

    function onGameStarted(game: Game) {
        console.log('Игра началась обработчик', {
            game
        })
        setGame(game);
        setRunGame(true)
    }

    function onGameEnded(result: GameResult) {
        console.log('Игра закончилась', {
            result
        });
        setGame(undefined);
        setRunGame(false);
    }

    return (
        <div>
            {
                runGame
                    ? <RealGamePage onGameEnded={onGameEnded} gameCommunicator={gameCommunicator} game={game!}/>
                    : <MainPage gamesRepository={gamesRepository} gameCommunicator={gameCommunicator} onGameStarted={onGameStarted}/>
            }
        </div>
    );
};


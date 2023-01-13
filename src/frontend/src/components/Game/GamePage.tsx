import React, {FC, useState} from 'react';
import GameProps from "./GameProps";
import RealGamePage from "./RealGame/RealGamePage";
import MainPage from "./MainPage/MainPage";
import GameResult from "../../models/gameResult";
import Game from "../../models/game";

export const GamePage: FC<GameProps> = ({gamesRepository, gameCommunicator}) => {
    const [game, setGame] = useState<Game>();

    function onGameStarted(game: Game) {
        setGame(game);
    }

    function onGameEnded(result: GameResult) {
        console.log('Игра закончилась', {
            result
        });
        setGame(undefined);
    }

    return (
        <div>
            {
                game
                    ? <RealGamePage onGameEnded={onGameEnded} gameCommunicator={gameCommunicator} game={game}/>
                    : <MainPage gamesRepository={gamesRepository} gameCommunicator={gameCommunicator} onGameStarted={onGameStarted}/>
            }
        </div>
    );
};


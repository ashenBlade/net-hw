import React, {FC, useEffect, useState} from 'react';
import GameProps from "./GameProps";
import Game from "../../models/game";
import {GameStatus} from "../../models/gameStatus";
import RealGamePage from "./RealGame/RealGamePage";
import MainPage from "./MainPage/MainPage";
import GameResult from "../../models/gameResult";

const Game: FC<GameProps> = ({gamesRepository, gameCommunicator}) => {
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

export default Game;

import React, {FC, useEffect, useReducer, useState} from 'react';
import RealGameProps from './RealGameProps';
import GameResult from "../../../models/gameResult";
import {GameSign} from "../../../models/gameSign";

enum GameFieldSign {
    None = '',
    X = 'X',
    O = 'O'
}

function getAlternateSign(sign: GameSign): GameSign {
    return sign === GameSign.O ? GameSign.X : GameSign.O;
}
const RealGamePage: FC<RealGameProps> = ({gameCommunicator, onGameEnded: parentGameEnded, game}) => {
    const [field, setField] = useState([
        [GameFieldSign.None, GameFieldSign.None, GameFieldSign.None],
        [GameFieldSign.None, GameFieldSign.None, GameFieldSign.None],
        [GameFieldSign.None, GameFieldSign.None, GameFieldSign.None]
    ]);
    const [currentSign, setCurrentSign] = useState(GameSign.O);

    const [, rerender] = useReducer(prevState => prevState + 1, 0);

    function onGameEnded(result: GameResult) {
        alert('Игра закончена. Пошел нахуй. Ваши очки: ' + result.myPoints + '. Очки соперника: ' + result.opponentPoints)
        parentGameEnded(result);
    }

    async function endGameAsync() {
        setUiFreeze(true);
        try {
            await gameCommunicator.endGameAsync();
        } finally {
            setUiFreeze(false);
        }
    }

    const isMyTurn = () => currentSign === game.mySign;
    const [uiFreeze, setUiFreeze] = useState(false);
    const toGameField = (sign: GameSign) =>
        sign === GameSign.X
            ? GameFieldSign.X
            : sign === GameSign.O
                ? GameFieldSign.O
                : GameFieldSign.None;

    function onStepMade(x: number, y: number, signMadeStep: GameSign) {
        console.log({
            x, y, signMadeStep
        })
        field[x][y] = toGameField(signMadeStep);
        setUiFreeze(signMadeStep === game.mySign);
        setCurrentSign(getAlternateSign(signMadeStep));
        rerender();
    }


    useEffect(() => {
        gameCommunicator.registerOnStepCallback(onStepMade);
        gameCommunicator.registerOnGameEndedCallback(onGameEnded);
        return () => {
            gameCommunicator.unregisterOnGameEndedCallback(onGameEnded);
            gameCommunicator.unregisterOnStepCallback(onStepMade);
        }
    }, [gameCommunicator])
    return (
        <div>
            <p>{
                isMyTurn() ? "Ваш ход" : "Ход противника"
            }</p>
            <p>
                Ваш знак: {game.mySign}
            </p>
            <table style={{
                border: '1px solid black'
            }}>
                {
                    field.map((row, i) => (
                        <tr>
                            {
                                row.map((cell, j) => (
                                    <td style={{
                                        padding: '10px',
                                        border: '1px solid black'
                                    }} onClick={async () => {
                                        let b = !isMyTurn();
                                        let b1 = cell !== GameFieldSign.None;
                                        console.log('fuck', {
                                            b, uiFreeze, b1
                                        })
                                        if (b || uiFreeze || b1) return;
                                        setUiFreeze(true);
                                        try {
                                            await gameCommunicator.makeStepAsync(i, j);
                                        } finally {
                                            setUiFreeze(false);
                                        }
                                    }}>
                                        {cell}
                                    </td>
                                ))
                            }
                        </tr>
                    ))
                }
            </table>

        </div>
    );
};

export default RealGamePage;

import React, {FC, useEffect, useReducer, useState} from 'react';
import RealGameProps from './RealGameProps';
import GameResult from "../../../models/gameResult";
import {GameSign} from "../../../models/gameSign";

enum GameField {
    None = '',
    X = 'X',
    O = 'O'
}

function getAlternateSign(sign: GameSign): GameSign {
    return sign === GameSign.O ? GameSign.X : GameSign.O;
}
const RealGamePage: FC<RealGameProps> = ({gameCommunicator, onGameEnded: parentGameEnded, game}) => {
    const [field, setField] = useState([
        [GameField.None, GameField.None, GameField.None],
        [GameField.None, GameField.None, GameField.None],
        [GameField.None, GameField.None, GameField.None]
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
            ? GameField.X
            : sign === GameSign.O
                ? GameField.O
                : GameField.None;

    function onStepMade(x: number, y: number, sign: GameSign) {
        field[x][y] = toGameField(sign);
        setUiFreeze(sign === game.mySign);
        setCurrentSign(getAlternateSign(sign));
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
            <table>
                {
                    field.map((row, i) => (
                        <tr>
                            {
                                row.map((cell, j) => (
                                    <td onClick={async () => {
                                        if (!isMyTurn() || uiFreeze || cell !== GameField.None) return;
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

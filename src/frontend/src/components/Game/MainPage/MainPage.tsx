import React, {FC, useEffect, useState} from 'react';
import MainPageProps from "./MainPageProps";
import Game from "../Game";
import {GameStatus} from "../../../models/gameStatus";

const MainPage: FC<MainPageProps> = ({onGameStarted: onGameStartedParent, gamesRepository, gameCommunicator}) => {
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

    return (
        <div>
            {
                allGames.map(g => (
                    <li>
                        {g.id} - {g.status}
                        {
                            g.status === GameStatus.Started
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

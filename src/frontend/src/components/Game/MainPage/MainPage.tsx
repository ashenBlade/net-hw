import React, {FC, useEffect, useReducer, useState} from 'react';
import MainPageProps from "./MainPageProps";
import {GameStatus} from "../../../models/gameStatus";
import Game from "../../../models/game";

const MainPage: FC<MainPageProps> = ({onGameStarted: onGameStartedParent, 
                                         gamesRepository, 
                                         gameCommunicator}) => {
    function onGameStarted(game: Game) {
        setFreeze(false);
        setIsGameFinding(false);
        onGameStartedParent(game);
    }
    
    const [,rerender] = useReducer(prevState => prevState + 1, 0);
    
    const [currentPage, setCurrentPage] = useState(1)
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
    const [isGameFinding, setIsGameFinding] = useState(false);
    
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
                alert('Не удалось присоединиться к игре')
                setFreeze(false);
            }
        }
    }
    
    
    async function createGame() {
        if (freeze) {
            return;
        }
        setFreeze(true);
        setIsGameFinding(true);
        await gamesRepository.createGameAsync(rank)
            .catch(() => setFreeze(false));
    }
    
    const [rank, setRank] = useState(0);
    
    const onListUpdateButtonClick = async () => {
        if (freeze || isGameFinding) return;
        setFreeze(true);

        try {
            const games = await gamesRepository.getGamesPagedAsync(currentPage, 20)
            setAllGames(games);
        } finally {
            setFreeze(false)
        }
        
    }
    
    return (
        <div>
            <p>
                {isGameFinding ? "Игра ищется. Жди" : ""}
            </p>
            <form>
                <label>
                    Максимальный ранг
                    <input type={'number'} value={rank} onChange={x => setRank(Number.parseInt(x.currentTarget.value))}/>
                </label>
                <button type={'button'} disabled={freeze || isGameFinding} onClick={createGame}>Создать игру</button>
            </form>
            <button onClick={() => {
                let games = allGames.sort((a, b) => a.startDate.toString() > b.startDate.toString() ? 1 : -1);
                console.log('По дате', {
                    games
                })
                
                setAllGames(games)
                rerender()
            }}>По дате</button>
            <button onClick={() => {
                setAllGames(allGames.sort((a, b) => a.status.toString() > b.status.toString() ? 1 : -1))
                rerender();
            }}>По статусу</button>
            <ul>{
                allGames.map(g => (
                    <li key={g.id}>
                        Id: {g.id} - Статус: {g.status} {g.startDate.toLocaleString()}
                        {
                            g.status === GameStatus.Created
                            && <button disabled={freeze || isGameFinding}
                                       onClick={async () => await onGameStartClick(g.id)}>
                                Присоединиться
                            </button>
                        }
                    </li>
                ))
            }</ul>
            <button onClick={onListUpdateButtonClick} disabled={freeze || isGameFinding}>
                Обновить список
            </button>
        </div>
    );
};

export default MainPage;

import IGamesRepository from "../interfaces/iGamesRepository";
import Game from "../models/game";
import {GameSign} from "../models/gameSign";
import {GameStatus} from "../models/gameStatus";

export default class BackendGamesRepository implements IGamesRepository {
    constructor(readonly serverUrl: string, 
                readonly jwt: string) {  }

    async createGameAsync(rank: number): Promise<void> {
        const url = `${this.serverUrl}/api/games`
        const response = await fetch(url, {
            method: 'POST',
            mode: 'cors',
            body: JSON.stringify({
                rank
            }),
            headers: {
                'Authorization': `Bearer ${this.jwt}`,
                'Content-Type': 'application/json'
            }
        })
        const json = await response.json();
        if (!response.ok) {
            console.log('Ошибка', json)
            throw new Error('Не удалось создать игру')
        }
    }

    async getGamesPagedAsync(page: number, size: number): Promise<Game[]> {
        const url = `${this.serverUrl}/api/games?page=${page}&size=${size}`;
        const response = await fetch (url, {
            method: 'GET',
            mode: 'cors',
            // credentials: 'include',
            headers: {
                'Authorization': `Bearer ${this.jwt}`
            }
        });
        if (!response.ok) {
            throw new Error('Ошибка при получении списка игр')
        }
        let games: any[] = await response.json();
        const mapped: Game[] = games.map((g: any) => ({
            startDate: new Date(Date.parse(g.startDate)),
            mySign: g.mySign === 'X' ? GameSign.X : GameSign.O,
            status: g.status === 'ended' ? GameStatus.Ended : g.status === 'created' ? GameStatus.Created : GameStatus.Playing,
            opponentName: g.opponentName,
            id: g.id
        })); 
        console.log('Полученые игры', {
            mapped
        })
        return mapped;
    }
    
    
}
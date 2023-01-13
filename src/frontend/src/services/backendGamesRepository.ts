import IGamesRepository from "../interfaces/iGamesRepository";
import Game from "../models/game";

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
        return await response.json();
    }
    
    
}
import IGamesRepository from "../interfaces/iGamesRepository";
import Game from "../models/game";

export default class BackendGamesRepository implements IGamesRepository {
    constructor(readonly serverUrl: string, 
                readonly jwt: string) {  }

    async getGamesPagedAsync(page: number, size: number): Promise<Game[]> {
        const url = `${this.serverUrl}/api/games?page=${page}&size=${size}`;
        const response = await fetch (url, {
            mode: 'cors',
            credentials: 'include',
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
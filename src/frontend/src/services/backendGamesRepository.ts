import IGamesRepository from "../interfaces/iGamesRepository";
import Game from "../models/game";

export default class BackendGamesRepository implements IGamesRepository {
    constructor(readonly serverUrl: string) {}

    async getGamesPagedAsync(page: number, size: number): Promise<Game[]> {
        const url = `${this.serverUrl}/games?page=${page}&size=${size}`;
        const response = await fetch (url, {
            mode: 'cors',
            credentials: 'include'
        });
        if (!response.ok) {
            throw new Error('Ошибка при получении списка игр')
        }
        return await response.json();
    }
}
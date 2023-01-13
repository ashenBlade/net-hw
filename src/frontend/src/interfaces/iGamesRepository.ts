import Game from "../models/game";

export default interface IGamesRepository {
    getGamesPagedAsync(page: number, size: number): Promise<Game[]>
    
    // Поставить в очередь запрос на создание игры
    createGameAsync(rank: number): Promise<void>
}
import Game from "../models/game";

export default interface IGamesRepository {
    getGamesPagedAsync(page: number, size: number): Promise<Game[]>
}
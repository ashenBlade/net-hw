import {GameStatus} from "./gameStatus";
import {GameSign} from "./gameSign";

export default interface Game {
    id: string,
    status: GameStatus,
    opponentName: string,
    /// Мой символ
    mySign: GameSign
}
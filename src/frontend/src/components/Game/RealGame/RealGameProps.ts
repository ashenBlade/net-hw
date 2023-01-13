import IGameCommunicator from "../../../interfaces/IGameCommunicator";
import GameResult from "../../../models/gameResult";
import Game from "../Game";

export default interface RealGameProps {
    gameCommunicator: IGameCommunicator;
    game: Game;
    onGameEnded: (result: GameResult) => void;
}
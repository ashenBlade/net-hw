import IGameCommunicator from "../../../interfaces/IGameCommunicator";
import GameResult from "../../../models/gameResult";
import Game from "../../../models/game";

export default interface RealGameProps {
    gameCommunicator: IGameCommunicator;
    game: Game;
    onGameEnded: (result: GameResult) => void;
}
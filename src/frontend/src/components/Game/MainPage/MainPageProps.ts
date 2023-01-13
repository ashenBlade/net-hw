import IGamesRepository from "../../../interfaces/iGamesRepository";
import IGameCommunicator from "../../../interfaces/IGameCommunicator";
import Game from "../../../models/game";

export default interface MainPageProps {
    gamesRepository: IGamesRepository
    onGameStarted: (game: Game) => void;
    gameCommunicator: IGameCommunicator
}
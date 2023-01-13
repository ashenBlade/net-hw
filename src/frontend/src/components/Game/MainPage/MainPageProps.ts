import IGamesRepository from "../../../interfaces/iGamesRepository";
import Game from "../Game";
import IGameCommunicator from "../../../interfaces/IGameCommunicator";

export default interface MainPageProps {
    gamesRepository: IGamesRepository
    onGameStarted: (game: Game) => void;
    gameCommunicator: IGameCommunicator
}
import IAuthorizer from "../../interfaces/IAuthorizer";
import IGameCommunicator from "../../interfaces/IGameCommunicator";
import IGamesRepository from "../../interfaces/iGamesRepository";

export default interface GameProps {
    authorizer: IAuthorizer
    gameCommunicator: IGameCommunicator
    gamesRepository: IGamesRepository
}
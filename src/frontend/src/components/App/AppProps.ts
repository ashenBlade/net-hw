import IAuthorizer from "../../interfaces/IAuthorizer";
import IGameCommunicator from "../../interfaces/IGameCommunicator";
import IGamesRepository from "../../interfaces/iGamesRepository";

export default interface AppProps {
    authorizer: IAuthorizer,
    communicator: IGameCommunicator,
    repository: IGamesRepository
}
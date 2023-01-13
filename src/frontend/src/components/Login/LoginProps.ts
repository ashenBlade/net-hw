import IAuthorizer from "../../interfaces/IAuthorizer";

export default interface LoginProps {
    onLoginedCallback: (jwt: string) => void;
    authorizer: IAuthorizer
}
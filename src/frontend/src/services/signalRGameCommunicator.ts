import {BaseGameCommunicator} from "./baseGameCommunicator";
import {HubConnection, HubConnectionBuilder, LogLevel} from "@microsoft/signalr";
import {FetchHttpClient} from "@microsoft/signalr/dist/esm/FetchHttpClient";
import {ConsoleLogger} from "@microsoft/signalr/dist/esm/Utils";
import {GameStatus} from "../models/gameStatus";
import {GameSign} from "../models/gameSign";

export default class SignalRGameCommunicator extends BaseGameCommunicator {
    static GameStartedFunction = 'GameStarted';
    static GameEndedFunction = 'GameEnded';

    static MakeStepFunction = 'MakeStep';
    static ConnectToGameFunction = 'ConnectToGame';
    static EndGameFunction = 'EndGame';

    connection: HubConnection
    jwt: string | null = null;
    
    constructor(readonly url: string,
                readonly gameEndpoint: string = '/game') {
        super();
        this.connection = new HubConnectionBuilder()
            .withUrl(this.endpoint, {
                withCredentials: true,
                httpClient: new FetchHttpClient(new ConsoleLogger(LogLevel.Debug))
            })
            .build();
    }
    
    setJwt(jwt: string) {
        this.jwt = jwt;
    }

    get endpoint() {
        return `${this.url}${this.gameEndpoint}`;
    }

    async open() {
        this.connection.on(SignalRGameCommunicator.GameStartedFunction, (gameId: string, opponent: string, sign: string) => {
            console.debug("Игра началась");
            this.onGameStarted({
                id: gameId,
                mySign: sign === 'O'
                    ? GameSign.O
                    : GameSign.X,
                opponentName: opponent,
                status: GameStatus.Started
            })
        })
        this.connection.on(SignalRGameCommunicator.MakeStepFunction, (x: number, y: number, sign: string) => {
            console.debug('Ход сделан', {
                x, y, sign
            })
            this.onStepMade(x, y, sign);
        })
        this.connection.on(SignalRGameCommunicator.GameEndedFunction, (myPoints: number, opponentPoints: number) => {
            console.error('Игра закончена', {
                myPoints, opponentPoints
            })
            this.onGameEnded({
                myPoints, opponentPoints
            })
        })
        await this.connection.start();
    }

    async stop() {
        await this.connection.stop();
    }

    async connectToGameAsync(gameId: string): Promise<boolean> {
        const result = await this.connection.invoke<boolean>(SignalRGameCommunicator.ConnectToGameFunction, gameId);
        return result;
    }

    async endGameAsync(): Promise<boolean> {
        const result = await this.connection.invoke<boolean>(SignalRGameCommunicator.EndGameFunction);
        return result;
    }

    async makeStepAsync(x: number, y: number): Promise<void> {
        await this.connection.send(SignalRGameCommunicator.MakeStepFunction, x, y);
    }

}
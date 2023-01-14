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
    
    constructor(readonly url: string,
                readonly jwt: string,
                readonly gameEndpoint: string = '/game') {
        super();
        this.connection = new HubConnectionBuilder()
            .withUrl(this.endpoint, {
                withCredentials: false,
                httpClient: new FetchHttpClient(new ConsoleLogger(LogLevel.Debug)),
                accessTokenFactory(): string | Promise<string> {
                    return jwt;
                }
            })
            .build();
    }
    
    get endpoint() {
        return `${this.url}${this.gameEndpoint}`;
    }

    async open() {
        this.connection.on(SignalRGameCommunicator.GameStartedFunction, 
            (gameId: string, opponent: string, sign: string, startDate: string) => {
            console.debug("Игра началась", {
                gameId, opponent, sign, startDate
            });
            
            this.onGameStarted({
                id: gameId,
                startDate: new Date(Date.parse(startDate)),
                mySign: sign === 'O'
                    ? GameSign.O
                    : GameSign.X,
                opponentName: opponent,
                status: GameStatus.Created
            })
        })
        this.connection.on(SignalRGameCommunicator.MakeStepFunction, 
            (x: number, y: number, sign: string) => {
            console.debug('Ход сделан', {
                x, y, sign
            })
            this.onStepMade(x, y, sign === 'O' 
                ? GameSign.O 
                : GameSign.X);
        })
        this.connection.on(SignalRGameCommunicator.GameEndedFunction, 
            (myPoints: number, opponentPoints: number) => {
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
        await this.connection.send(SignalRGameCommunicator.ConnectToGameFunction, gameId);
        return true;
    }

    async endGameAsync(): Promise<boolean> {
        await this.connection.send(SignalRGameCommunicator.EndGameFunction);
        return true;
    }

    async makeStepAsync(x: number, y: number): Promise<void> {
        await this.connection.send(SignalRGameCommunicator.MakeStepFunction, x, y);
    }

}
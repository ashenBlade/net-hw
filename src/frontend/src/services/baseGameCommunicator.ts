import IGameCommunicator from "../interfaces/IGameCommunicator";
import {GameStartedCallback} from "../interfaces/gameStartedCallback";
import {StepMadeCallback} from "../interfaces/stepMadeCallback";
import {GameEndedCallback} from "../interfaces/gameEndedCallback";
import GameResult from "../models/gameResult";
import Game from "../models/game";
import {GameSign} from "../models/gameSign";

export abstract class BaseGameCommunicator implements IGameCommunicator {
    private gameEndedCbs: GameEndedCallback[] = []
    private gameStartedCbs: GameStartedCallback[] = []
    private stepMadeCbs: StepMadeCallback[] = []

    abstract connectToGameAsync(gameId: string): Promise<boolean>;

    abstract endGameAsync(): Promise<boolean>;

    abstract makeStepAsync(x: number, y: number): Promise<void>;

    private basePushCb<T>(cb: T, array: T[]): void {
        const index = array.indexOf(cb);
        if (index !== -1) {
            array.push(cb)
        }
    }

    private baseUnregister<T>(cb: T, array: T[]): void {
        const index = array.indexOf(cb);
        if (index !== -1) {
            array.splice(index, 1);
        }
    }

    registerOnGameEndedCallback(cb: GameEndedCallback): void {
        this.basePushCb(cb, this.gameEndedCbs);
    }

    onGameEnded(result: GameResult) {
        this.gameEndedCbs.forEach(cb => cb(result));
    }

    registerOnGameStartedCallback(cb: GameStartedCallback): void {
        this.basePushCb(cb, this.gameStartedCbs);
    }

    onGameStarted(game: Game) {
        this.gameStartedCbs.forEach(cb => cb(game));
    }

    registerOnStepCallback(cb: StepMadeCallback): void {
        this.basePushCb(cb, this.stepMadeCbs);
    }

    onStepMade(x: number, y: number, sign: GameSign) {
        this.stepMadeCbs.forEach(cb => cb(x, y, sign));
    }

    unregisterOnGameEndedCallback(cb: GameEndedCallback): void {
        this.baseUnregister(cb, this.gameEndedCbs);
    }

    unregisterOnGameStartedCallback(cb: GameStartedCallback): void {
        this.baseUnregister(cb, this.gameStartedCbs);
    }

    unregisterOnStepCallback(cb: StepMadeCallback): void {
        this.baseUnregister(cb, this.stepMadeCbs);
    }

}
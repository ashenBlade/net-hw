import {GameStartedCallback} from "./gameStartedCallback";
import {StepMadeCallback} from "./stepMadeCallback";
import {GameEndedCallback} from "./gameEndedCallback";

export default interface IGameCommunicator {
    connectToGameAsync(gameId: string): Promise<boolean>
    endGameAsync(): Promise<boolean>
    makeStepAsync(x: number, y: number): Promise<void>

    registerOnGameStartedCallback(cb: GameStartedCallback): void
    unregisterOnGameStartedCallback(cb: GameStartedCallback): void

    registerOnGameEndedCallback(cb: GameEndedCallback): void
    unregisterOnGameEndedCallback(cb: GameEndedCallback): void

    registerOnStepCallback(cb: StepMadeCallback): void
    unregisterOnStepCallback(cb: StepMadeCallback): void

}
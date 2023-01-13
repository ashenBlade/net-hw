import {GameSign} from "../models/gameSign";

export type StepMadeCallback = (x: number, y: number, sign: GameSign) => void;

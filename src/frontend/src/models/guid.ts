export default class Guid {
    constructor(readonly value: string) {
        if (!/^[0-9a-f]{8}-[0-9a-f]{4}-[0-5][0-9a-f]{3}-[089ab][0-9a-f]{3}-[0-9a-f]{12}$/i.test(value)) {
            throw new Error(`Provided GUID (${value}) is not valid`)
        }
    }
    static generate(): Guid {
        return new Guid('xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
            const r = Math.random() * 16 | 0;
            const v = c == 'x'
                ? r
                : (r & 0x3 | 0x8);
            return v.toString(16);
        }));
    }

    static parse(value: string): Guid {
        return new Guid(value);
    }

    public toString(): string {
        return this.value;
    }
}
import IAuthorizer from "../interfaces/IAuthorizer";

export default class BackendAuthorizer implements IAuthorizer {
    jwt: string | null = null;

    constructor(readonly backendUrl: string) {
    }

    setJwt(jwt: string): void {
        this.jwt = jwt;
    }

    getJwt(): string | null {
        return this.jwt;
    }

    async loginAsync(username: string, password: string): Promise<string> {
        const loginUrl = `${this.backendUrl}/api/users/login`;
        const response = await fetch(loginUrl, {
            method: 'POST',
            body: JSON.stringify({
                username, password
            }),
            mode: 'cors',
            headers: {
                'Content-Type': 'application/json'
            }
            // credentials: 'include',
        })
        if (!response.ok) {
            throw new Error('От сервера вернулся неверный ответ')
        }
        const json = await response.json()
        const token = json.accessToken;
        if (!token) {
            throw new Error('От сервера пришел неверный ответ')
        }
        this.jwt = token;
        return token;
    }

    async registerAsync(username: string, password: string): Promise<string> {
        const loginUrl = `${this.backendUrl}/api/users/register`;
        const response = await fetch(loginUrl, {
            method: 'POST',
            body: JSON.stringify({
                username, password
            }),
            mode: 'cors',
            headers: {
                'Content-Type': 'application/json'
            }
        })
        if (!response.ok) {
            throw new Error('От сервера вернулся неверный ответ')
        }
        const json = await response.json()
        console.log(json)
        const token = json.accessToken;
        if (!token) {
            throw new Error('От сервера пришел неверный ответ')
        }
        this.jwt = token;
        return token;
    }

}
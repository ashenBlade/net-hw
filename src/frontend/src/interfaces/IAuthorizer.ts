export default interface IAuthorizer {
    registerAsync(username: string, password: string): Promise<string>
    loginAsync(username: string, password: string): Promise<string>
    getJwt(): string | null
    setJwt(jwt: string): void
}
import { request } from 'superagent';
import { JWTTokens } from 'ClientApp/models/jwttokens';

export class BackendService {
    async login(userName: string, password: string) {
        var responseBody = await this._post('/api/auth/login', { userName: userName, password: password });
        return responseBody as JWTTokens;
    }

    private _get(url: string): any {
        request.get(`${url}`).use(this._setTokenHeader).then((response) => response.body);
    }

    private _post(url: string, body: any): any {
        request.post(`${url}`, body).use(this._setTokenHeader).then((response) => response.body);
    }

    private _setTokenHeader(req) {
        if (this._token) {
            req.set('Authorization', `Bearer ${this._token}`);
        }
    }

    private get _token(): string {
        const accessToken = localStorage.getItem(Constants.AccessTokenKey);
        return accessToken;
    }
}

export const backendService = new BackendService();
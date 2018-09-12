import { JWTTokens } from '../models/jwttokens';
import { Constants } from '../constants';

class BackendService {
    login(userName: string, password: string) {
        return this._post('/auth/login', { userName: userName, password: password });
        //return responseBody as JWTTokens;
    }

    private _get(url: string): any {
        return fetch(url, {
            headers: this._getTokenHeader()
        }).then(response => response.body);
    }

    private _post(url: string, body: any): any {
        return fetch(url, {
            method: 'POST',
            body: JSON.stringify(body),
            headers: this._getTokenHeader()
        }).then(response => response.body);
    }

    private _getTokenHeader() {
        if (this._token) {
            return {
                "Authorization": `Bearer ${this._token}`,
                'Content-Type': 'application/json'
            };
        } else {
            return {
                'Content-Type': 'application/json'
            };
        }
    }

    private get _token(): string {
        const accessToken = localStorage.getItem(Constants.AccessTokenKey);
        return accessToken;
    }
}

export default new BackendService();
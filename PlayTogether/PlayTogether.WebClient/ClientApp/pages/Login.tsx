import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import { AuthState } from '../store/auth/reducers';
import { actionCreators, AuthActionTypes } from '../store/auth/actions';
import { Redirect } from 'react-router';

type LoginProps =
    AuthState
    & typeof actionCreators
    & RouteComponentProps<{}>;

class Login extends React.Component<LoginProps, {}> {
    constructor(props: any) {
        super(props);
        this.props.logout();

        this.state = {
            email: '',
            password: ''
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    public render() {
        const { isLogining, isLoggedIn } = this.props;
        const { email, password }: any = this.state;

        if (isLoggedIn) {
            return <Redirect to="/" />
        }

        return <div>
            <form onSubmit={this.handleSubmit}>
                <div className="form-group">
                    <input className="form-control" name="email" type="text" placeholder="E-mail"
                        value={email} onChange={this.handleChange} />
                </div>
                <div className="form-group">
                    <input className="form-control" name="password" type="password" placeholder="Пароль"
                        value={password} onChange={this.handleChange} />
                </div>
                {isLogining && <p>Загрузка...</p>}
                {!isLogining &&
                    <button className="btn btn-default" type="submit">Войти</button>
                }
            </form>
        </div>;
    }

    handleSubmit(e) {
        e.preventDefault();

        const { email, password }: any = this.state;
        if (email && password) {
            this.props.login(email, password);
        }
    }

    handleChange(e: any) {
        const { name, value } = e.target;
        this.setState({ [name]: value });
    }
}
export default connect(
    (state: ApplicationState) => state.auth,
    actionCreators
)(Login) as typeof Login;
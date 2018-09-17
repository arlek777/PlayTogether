import * as React from 'react';
import { NavMenu } from './NavMenu';
import { Switch, Route, RouteComponentProps } from 'react-router';
import Home from '../pages/Home';
import Login from '../pages/Login';
import { AuthState } from '../store/auth/reducers';
import { actionCreators } from '../store/auth/actions';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import PrivateRoute from './PrivateRoute';

type AppProps =
    AuthState
    & typeof actionCreators
    & RouteComponentProps<{}>;

class App extends React.Component<AppProps, {}> {
    public render() {
        const { userName, isLoggedIn } = this.props;
   
        return <div className='container-fluid'>
            <div className='row'>
                <div className='col-sm-3'>
                    <NavMenu userName={userName} isLoggedIn={isLoggedIn} />
                </div>
                <div className='col-sm-9'>
                    <Switch>
                        <PrivateRoute exact path="/" component={Home} />
                        <Route path="/login" component={Login} />
                    </Switch>
                </div>
            </div>
        </div>;
    }
}
export default connect(
    (state: ApplicationState) => state.auth,
    actionCreators
)(App) as typeof App;

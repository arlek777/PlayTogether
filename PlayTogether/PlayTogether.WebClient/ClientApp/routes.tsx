import * as React from 'react';
import { Route, Switch } from 'react-router-dom';
import Home from './pages/Home';
import Login from './pages/Login';
import PrivateRoute from 'ClientApp/components/PrivateRoute';

export const routes = <Switch>
    <PrivateRoute path='/' component={ App } />
    <Route path='/login' component={ Login } />
</Switch>;

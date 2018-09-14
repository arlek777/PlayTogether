import * as React from 'react';
import { Route, Redirect, RouteComponentProps, RouteProps } from 'react-router-dom';
import { Component } from 'react';
import { Constants } from '../constants';

export default class PrivateRoute extends React.Component<RouteProps, {}> {
    public render() {
        const { ...rest } = this.props;

        if (localStorage.getItem(Constants.AccessTokenKey)) {
            return <Route {...rest} />;
        } else {
            return <Redirect to={{ pathname: '/login' }} />;
        }
    }
}
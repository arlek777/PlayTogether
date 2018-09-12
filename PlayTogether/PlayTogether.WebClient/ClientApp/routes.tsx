import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import Home from './components/Home';
import Login from './components/Login';

export const routes = <Layout>
    <Route exact path='/' component={ Home } />
    <Route path='/login' component={ Login } />
</Layout>;

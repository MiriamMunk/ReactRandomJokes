import React, { Component } from 'react';
import { Route } from 'react-router-dom';
import Layout from './Layout';
import HomePage from './Pages/Home';
import LogIn from './Pages/LogIn';
import Logout from './Pages/logOut';
import SignUp from './Pages/SignUp';
import ViewAll from './Pages/ViewAll';
import { UserContextComponent } from './UserContext';

export default class App extends Component {
    render() {
        return (
            <UserContextComponent>
                <Layout>
                    <Route exact path='/' component={HomePage} />
                    <Route exact path='/viewAll' component={ViewAll} />
                    <Route exact path='/signUp' component={SignUp} />
                    <Route exact path='/login' component={LogIn} />
                    <Route exact path='/logout' component={Logout} />

                </Layout>
            </UserContextComponent>
        );
    }
}
import * as React from 'react';
import { Link, RouteComponentProps, NavLink } from 'react-router-dom';

interface NavMenuProps {
    userName: string;
    isLoggedIn: boolean;
}

export class NavMenu extends React.Component<NavMenuProps, {}> {
    public render() {
        const { userName, isLoggedIn } = this.props;

        return <div className='main-nav'>
            <div className='navbar navbar-inverse'>
                <div className='navbar-header'>
                    <button type='button' className='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'>
                        <span className='sr-only'>Toggle navigation</span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                    </button>
                    <Link className='navbar-brand' to={'/'}>PlayTogether.WebClient</Link>
                </div>
                <div className='clearfix'></div>
                <div className='navbar-collapse collapse'>
                    <ul className='nav navbar-nav'>
                        (isLoggedIn &&
                        <li>
                            <NavLink exact to={'/'} activeClassName='active'>
                                Home
                            </NavLink>
                        </li>)
                        <li>
                            <NavLink to={'/login'} activeClassName='active'>
                                Login
                            </NavLink>
                        </li>
                    </ul>
                </div>
            </div>
        </div>;
    }
}

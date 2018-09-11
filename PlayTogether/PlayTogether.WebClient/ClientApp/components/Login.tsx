import * as React from 'react';
import { RouteComponentProps } from "react-router-dom";

export default class Login extends React.Component<RouteComponentProps<{}>, {}> {
    constructor(props: any) {
        super(props);
        this.state = { email: '', password: '' };
    }

    public render() {
        return <div>
                   <form onSubmit={this.submit}>
                       <div className="form-group">
                    <input className="form-control" name="email" type="text" placeholder="E-mail"
                        value="test" onChange={this.handleInputChange}/>
                       </div>
                       <div className="form-group">
                    <input className="form-control" name="password" type="password" placeholder="Пароль"
                        value="test" onChange={this.handleInputChange}/>
                       </div>
                       <button className="btn btn-default" type="submit">Войти</button>
                   </form>
               </div>;
    }

    submit() {
        
    }

    handleInputChange(event: any) {
        const target = event.target;
        const name = target.name;
        const value = target.value;

        this.setState({
            [name]: value
        });
    }
}
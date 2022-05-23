class LayoutForm extends React.Component {
    render() {
        return (
            <div className="layoutForm"><div className="accountBg"><div className="wrapper-page"><LoginForm /></div></div></div>
        );
    }
}

class LoginForm extends React.Component {
    constructor(props) {
        super(props);
        this.state1 = { value: '' };
        this.state2 = { value: '' };

        
        this.handleSubmit = this.h
        andleSubmit.bind(this);
    }

    

    handleSubmit(event) {
        event.preventDefault();
    }
    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <div className="card">
                    <div className="card-body">

                        <div className="text-center mt-0 m-b-15">
                            <a href="/" className="logo logo-admin"><img src="/images/teambox-logo.png" height="58" alt="logo" /></a>
                        </div>

                        <div className="p-3">

                            <div className="alert alert-info" role="alert">
                                Introduzca sus credenciales de acceso
                                <button type="button" className="close" data-dismiss="alert" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>

                            <div className="form-group row">
                                <div className="col-12">
                                    <input type="text" value={this.state1.value}  className="form-control"/>
                                </div>
                            </div>

                            <div className="form-group row">
                                <div className="col-12">
                                    <input type="text" value={this.state2.value}  className="form-control" />
                                </div>
                            </div>

                            <div className="form-group text-center row m-t-20">
                                <div className="col-12">
                                    <input type="submit" className="btn btn-primary submit-btn btn-block" value="Iniciar sesión"/>
                                </div>
                            </div>
                            
                        </div>
                    </div>

                </div>
            </form>
        );
    }
}

ReactDOM.render(
    React.createElement(LayoutForm, null),
    document.getElementById('content'),
);
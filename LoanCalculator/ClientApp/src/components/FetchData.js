import React, { Component } from 'react';

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = {
            loanAmount: 100000,
            loanLengthInYears: 15,
            paybackPlan: { payments: [] },
            loading: true
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.getPaybackPlan = this.getPaybackPlan.bind(this);
    }

    componentDidMount() {
    }

    handleInputChange(event) {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;
        const name = target.name;

        this.setState({
            [name]: value
        });
    }

    static renderForecastsTable(payments) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Month</th>
                        <th>Payment</th>
                    </tr>
                </thead>
                <tbody>
                    {payments.map((p, i) =>
                        <tr key={i}>
                            <td>{i + 1}</td>
                            <td>{p}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchData.renderForecastsTable(this.state.paybackPlan.payments);

        return (
            <div>
                <h1 id="tabelLabel" >Loan calculator</h1>
                <p>This component calculates payback plan for loans.</p>
                <input type="number" name="loanAmount" value={this.state.loanAmount} onChange={this.handleInputChange}></input>
                <input type="number" name="loanLengthInYears" value={this.state.loanLengthInYears} onChange={this.handleInputChange}></input>
                <input type="submit" onClick={this.getPaybackPlan} />
                {contents}
            </div>
        );
    }

    async getPaybackPlan(e) {
        e.preventDefault();

        this.setState({ paybackPlan: [], loading: true });
        const respone = await fetch(`loancalculation?loanAmount=${this.state.loanAmount}&loanLengthInYears=${this.state.loanLengthInYears}`);
        const data = await respone.json();
        this.setState({ paybackPlan: data, loading: false });
    }
}

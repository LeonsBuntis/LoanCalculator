import React, { Component } from 'react';

export class LoanCalculator extends Component {
    static displayName = LoanCalculator.name;

    constructor(props) {
        super(props);
        this.state = {
            loanAmount: 100000,
            loanLengthInYears: 15,
            loanType: 'housing',
            paybackPlan: { payments: [] },
            loading: false
        };

        this.handleInputChange = this.handleInputChange.bind(this);
        this.getPaybackPlan = this.getPaybackPlan.bind(this);
    }

    componentDidMount() { }

    handleInputChange(event) {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;
        const name = target.name;

        this.setState({
            [name]: value
        });
    }

    static renderPaybackPlan(payments) {
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
            : this.state.paybackPlan.payments.length > 0
                ? LoanCalculator.renderPaybackPlan(this.state.paybackPlan.payments)
                : <></>;

        return (
            <div>
                <h1 id="tabelLabel" >Loan calculator</h1>
                <p>This component calculates payback plan for loans.</p>
                <form>
                    <div className="form-group">
                        <label>Loan type</label>
                        <select className="form-control"
                            aria-label="Loan type"
                            name="loanType"
                            value={this.state.loanType}
                            onChange={this.handleInputChange}>
                            <option value="housing">Housing loan</option>
                        </select>
                    </div>
                    <div className="form-group">
                        <label>Loan length in years</label>
                        <input type="number"
                            className="form-control"
                            min="1"
                            max="30"
                            name="loanLengthInYears"
                            value={this.state.loanLengthInYears}
                            onChange={this.handleInputChange}
                            placeholder="Loan length in years" />
                    </div>
                    <label>Loan amount</label>
                    <div className="input-group mb-3">
                        <div className="input-group-prepend">
                            <span className="input-group-text">$</span>
                        </div>
                        <input type="number"
                            className="form-control"
                            min="1"
                            max="1000000"
                            name="loanAmount"
                            value={this.state.loanAmount}
                            onChange={this.handleInputChange}
                            aria-label="Loan amount" />
                    </div>

                    <input type="submit" className="btn btn-primary" onClick={this.getPaybackPlan} />
                </form>

                <div className="mt-5">
                    {contents}
                </div>
            </div>
        );
    }

    async getPaybackPlan(e) {
        e.preventDefault();

        this.setState({ paybackPlan: [], loading: true });
        var uri = `loancalculation`
            + `?loanAmount=${this.state.loanAmount}`
            + `&loanLengthInYears=${this.state.loanLengthInYears}`
            + `&loanType=${this.state.loanType}`;
        const respone = await fetch(uri);
        const data = await respone.json();
        this.setState({ paybackPlan: data, loading: false });
    }
}

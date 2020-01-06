import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';
import { ApplicationState } from '../store';
import * as AllGamesStore from '../store/AllGames';

// At runtime, Redux will merge together...
type GetAllGamesProps =
    AllGamesStore.AllGamesServiceState // ... state we've requested from the Redux store
    & typeof AllGamesStore.actionCreators; // ... plus action creators we've requested

class FetchData extends React.PureComponent<GetAllGamesProps> {
  // This method is called when the component is first added to the document
  public componentDidMount() {
    this.ensureDataFetched();
  }

  // This method is called when the route parameters change
  public componentDidUpdate() {
    this.ensureDataFetched();
  }

  public render() {
    return (
      <React.Fragment>
        <h1 id="tabelLabel">Weather forecast</h1>
        <p>This component demonstrates fetching data from the server and working with URL parameters.</p>
        {this.renderForecastsTable()}
      </React.Fragment>
    );
  }

  private ensureDataFetched() {
    this.props.requestAllGames();
  }

  private renderForecastsTable() {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Title</th>
          </tr>
        </thead>
        <tbody>
          {this.props.games.map((game: AllGamesStore.Game) =>
            <tr key={game.id}>
              <td>{game.displayTitle}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }
}

export default connect(
  (state: ApplicationState) => state.allGames, // Selects which state properties are merged into the component's props
  AllGamesStore.actionCreators // Selects which action creators are merged into the component's props
)(FetchData as any);

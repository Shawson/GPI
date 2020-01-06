import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface AllGamesServiceState {
    isLoading: boolean;
    games: Game[];
}

export interface Game {
    id: string;
    displayTitle: string;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestAllGamesAction {
    type: 'REQUEST_ALL_GAMES';
}

interface ReceiveAllGamesAction {
    type: 'RECEIVE_ALL_GAMES';
    games: Game[];
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestAllGamesAction | ReceiveAllGamesAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestAllGames: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.allGames) {
            fetch(`http://localhost:5001/api/v1/GameSearch`)
                .then(response => response.json() as Promise<Game[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_ALL_GAMES', games: data });
                });

            dispatch({ type: 'REQUEST_ALL_GAMES' });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: AllGamesServiceState = { games: [], isLoading: false };

export const reducer: Reducer<AllGamesServiceState> = (state: AllGamesServiceState | undefined, incomingAction: Action): AllGamesServiceState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_ALL_GAMES':
            return {
                games: state.games,
                isLoading: true
            };
        case 'RECEIVE_ALL_GAMES':
            return {
                games: action.games,
                isLoading: false
            };
    }
};

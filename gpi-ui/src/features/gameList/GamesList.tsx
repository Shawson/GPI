import React from 'react'

import { Game } from 'api/githubAPI'
import { GameListItem } from './gameListItem'

import styles from './GamesList.module.css'

interface Props {
  games: Game[]
}

export const GamesList = ({ games }: Props) => {
  const renderedIssues = games.map(game => (
    <li key={game.id}>
      <GameListItem {...game} />
    </li>
  ))

  return <ul className={styles.issuesList}>{renderedIssues}</ul>
}

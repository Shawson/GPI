import React, { MouseEvent } from 'react'

import { Game } from 'api/githubAPI'
import { shorten } from 'utils/stringUtils'

import styles from './gameListItem.module.css'

type Props = Game 

export const GameListItem = ({
  displayName
}: Props) => {
  const onIssueClicked = (e: MouseEvent) => {
    e.preventDefault()
    e.stopPropagation()
  }


  return (
    <div className={styles.issue}>
      
      <div className="issue__body">
        <a href="#comments" onClick={onIssueClicked}>
          <span className={styles.number}>#{displayName}</span>
        </a>

      </div>
    </div>
  )
}

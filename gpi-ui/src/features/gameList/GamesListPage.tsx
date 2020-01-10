import React, { useState, useEffect } from 'react'

import { getAllGames, Game } from 'api/githubAPI'

import { GamesPageHeader } from './GamesPageHeader'
import { GamesList } from './GamesList'

export const GamesListPage = ({}) => {
  const [games, setGames] = useState<Game[]>([])
  const [numIssues, setNumIssues] = useState<number>(-1)
  const [isLoading, setIsLoading] = useState<boolean>(false)
  const [gamesError, setGamesError] = useState<Error | null>(null)


  useEffect(() => {
    async function fetchEverything() {
      async function fetchIssues() {
        const games = await getAllGames()
        setGames(games)
      }

      try {
        await fetchIssues()
        setGamesError(null)
      } catch (err) {
        console.error(err)
        setGamesError(err)
      } finally {
        setIsLoading(false)
      }
    }

    setIsLoading(true)

    fetchEverything()
  })

  if (gamesError) {
    return (
      <div>
        <h1>Something went wrong...</h1>
        <div>{gamesError.toString()}</div>
      </div>
    )
  }

  let renderedList = isLoading ? (
    <h3>Loading...</h3>
  ) : (
    <GamesList games={games} />
  )

  return (
    <div id="issue-list-page">
      <GamesPageHeader />
      {renderedList}
    </div>
  )
}

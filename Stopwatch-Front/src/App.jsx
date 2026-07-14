import { useEffect, useState } from 'react'
import './App.css'

function App() {

  const [status, setStatus] = useState('Idle')
  const [elapsedTime, setElapsedTime] = useState('00:00:00')

  useEffect(() => {
    fetch('http://localhost:5168/api/stopwatch/status')
      .then(response => response.json())
      .then(data => {
        setStatus(data.state)
        setElapsedTime(data.elapsedTime)
      })
      .catch(error => {
        console.error('Erro ao acessar a API:', error)
      })
  }, [])


  useEffect(() => {
    if (status !== 'Running') return

    const interval = setInterval(() => {
      fetch('http://localhost:5168/api/stopwatch/status')
        .then(response => response.json())
        .then(data => {
          setStatus(data.state)
          setElapsedTime(data.elapsedTime)
        })
    }, 100)

    return () => clearInterval(interval)
  }, [status])


  function handleStart(){
    fetch('http://localhost:5168/api/stopwatch/start', {
      method: 'POST'
  })
    .then(response => response.json())
    .then(() => {
      setStatus('Running')
    })
    .catch(error => {
      console.error('Erro ao acessar a API:', error)
    })
  }

  function handlePause(){
    fetch('http://localhost:5168/api/stopwatch/paused', {
      method: 'POST'
  })
    .then(response => response.json())
    .then(() => {
      setStatus('Paused')
    })
    .catch(error => {
      console.error('Erro ao acessar a API:', error)
    })
  }

  function handleReset(){
    fetch('http://localhost:5168/api/stopwatch/reset', {
      method: 'POST'
  })
    .then(response => response.json())
    .then(() => {
      setStatus('Idle')
    })
    .catch(error => {
      console.error('Erro ao acessar a API:', error)
    })
  }

  return (
    <main>
      <h1>Stopwatch</h1>

      <h2>{elapsedTime}</h2>

      <p>Status: {status}</p>

      <button onClick={handleStart}>
        Start
      </button>
      <button onClick={handlePause}>
        Pause
      </button>
      <button onClick={handleReset}>
        Reset
      </button>
    </main>

  )
}

export default App

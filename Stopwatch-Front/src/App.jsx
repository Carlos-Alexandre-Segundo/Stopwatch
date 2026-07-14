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
        setElapsedTime(formatElapsedTime(data.elapsedTime))
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
          setElapsedTime(formatElapsedTime(data.elapsedTime))
        })
    }, 1000)

    return () => clearInterval(interval)
  }, [status])


  function formatElapsedTime(value) {
    if(!value) return '00:00:00'

     return value.substring(0, 8)
    }

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
    fetch('http://localhost:5168/api/stopwatch/pause', {
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

  function handleResume(){
    fetch('http://localhost:5168/api/stopwatch/resume', {
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

  function handleReset(){
    fetch('http://localhost:5168/api/stopwatch/reset', {
      method: 'POST'
  })
    .then(response => response.json())
    .then(() => {
      setStatus('Idle')
      setElapsedTime('00:00:00')
    })
    .catch(error => {
      console.error('Erro ao acessar a API:', error)
    })

     return () => clearInterval(interval)
    }

  return (
    <main>
      <h1>Stopwatch</h1>

      <h2>{elapsedTime}</h2>

      <p>Status: {status}</p>

      <button onClick={handleStart}>
        Start
      </button>
      <button onClick={handleResume}>
        Resume
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

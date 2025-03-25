import { Routes } from 'react-router'
import './App.scss'
import { Route } from 'react-router'
import { NavigationBar } from './components/NavigationBar/NavigationBar'
import { Admin } from './components/pages/Admin/Admin'
import { Landing } from './components/pages/Landing/Landing'
import { ReportSighting } from './components/pages/ReportSighting/ReportSighting'
import { ViewSightings } from './components/pages/ViewSightings/ViewSightings'


function App() {
  return (
    <>
      <NavigationBar />
      <Routes>
        <Route path="/"  element={<Landing />} />
        <Route path="/ReportSighting"  element={<ReportSighting/>} />
        <Route path="/ViewSightings"  element={<ViewSightings/>} />
        <Route path="/Admin"  element={<Admin/>} />
      </Routes>
    </>
  )
}

export default App

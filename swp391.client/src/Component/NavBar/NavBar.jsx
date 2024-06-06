import { Component } from 'react'
import './NavBar.css'
import { Link } from 'react-router-dom'
import "../../index.css"
export default class NavBar1 extends Component {
  render() {
    return (
      <nav className='container'>
      <h1>Pet-ternary</h1>
      <p>Purr-fectly Healthy, Woof-tastically Happy</p>
            <ul>
                <li><a href='#news'>News</a></li>
                <li><a href='#aboutus'>About Us</a></li>
                <li><a href='#healthcareservice'>HealthCare Service</a></li>
                <li><a href='#contact'>Contact</a></li>                             
            </ul>
           <Link to="/login"><button  className='btn'>Sign In</button></Link> 
      </nav>
    )
  }
}

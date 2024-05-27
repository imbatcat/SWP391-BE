import { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [data, setData] = useState([]);
    useEffect(() => {
        async function fetchData() {
            try {
                const response = await fetch('https://localhost:7206/api/Accounts');
                if (!response.ok) {
                    throw new Error("Error fetching data");
                }
                const json = await response.json();
                setData(json); // Ensure setData is correctly referenced
                console.log(json);
            } catch (error) {
                console.error(error.message);
            }
        }

        fetchData(); // Call the async function to fetch data
    }, []); // Empty dependency array means this effect runs once on mount
    return (<div>
        {data.map((item, index) => (
            <div key={index}>
                {/* Render each item property here */}
                <p>{JSON.stringify(item)}</p> {/* Replace propertyName with the actual property names of your data items */}
            </div>
        ))}
        </div>
    );
}

export default App;
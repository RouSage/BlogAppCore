import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import FetchCategory from './components/Categories/FetchCategory';
import CreateCategory from './components/Categories/CreateCategory';
import EditCategory from './components/Categories/EditCategory';

const App = () => (
  <Layout>
    <Route exact path="/" component={Home} />
    <Route path="/categories" component={FetchCategory} />
    <Route path="/create-category" component={CreateCategory} />
    <Route path="/edit-category/:id" component={EditCategory} />
  </Layout>
);

export default App;
// export default class App extends Component {
//   static displayName = App.name;

//   render() {
//     return (
//       <Layout>
//         <Route exact path="/" component={Home} />
//         <Route path="/categories" component={FetchCategory} />
//         <Route path="/create-category" component={CreateCategory} />
//         <Route path="/edit-category/:id" component={EditCategory} />
//       </Layout>
//     );
//   }
// }

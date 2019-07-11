import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import { CreateCategory, EditCategory, CategoryTable } from './components/categories';

const App = () => (
  <Layout>
    <Route exact path="/" component={Home} />
    <Route path="/categories" component={CategoryTable} />
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

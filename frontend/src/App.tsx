import React from 'react';
import './App.css';
import {createBrowserRouter, RouterProvider} from "react-router-dom";
import {RegistrationPage} from "./pages/RegistrationPage";
import {AuthorizationPage} from "./pages/AuthorizationPage";
import {HomePage} from "./pages/HomePage";
import {ChatPage} from "./pages/ChatPage";

const App = () => {

  const router = createBrowserRouter([
    { path: "/", element: <HomePage />},
    { path: '/registration', element: <RegistrationPage /> },
    { path: '/authorization', element: <AuthorizationPage /> },
    { path: '/chat', element: <ChatPage /> },
  ])

  return (
      <RouterProvider router={router} />
  )
}

export default App;
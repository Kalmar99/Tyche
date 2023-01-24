import React from 'react';

export const Layout : React.FC<any> = (props) => {
  return (
    <div>
          {props.children}
      </div>
  )
}

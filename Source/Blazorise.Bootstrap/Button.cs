﻿#region Using directives
using Blazorise.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
#endregion

namespace Blazorise.Bootstrap
{
    public class Button : Blazorise.Button
    {
        #region Methods

        protected override void BuildRenderTree( RenderTreeBuilder builder )
        {
            builder
                .OpenElement( Type.ToButtonTagName() )
                .Id( ElementId )
                .Type( Type.ToButtonTypeString() )
                .Class( ClassNames )
                .Style( StyleNames )
                .Disabled( Disabled )
                .AriaPressed( Active )
                .TabIndex( TabIndex );

            if ( Type == ButtonType.Link )
            {
                builder
                    .Role( "button" )
                    .Href( To )
                    .Target( Target );

                if ( Disabled )
                {
                    builder
                        .TabIndex( -1 )
                        .AriaDisabled( "true" );
                }
            }

            builder.OnClick( this, EventCallback.Factory.Create<MouseEventArgs>( this, ClickHandler ) );
            builder.OnClickPreventDefault( Type == ButtonType.Link && To != null && To.StartsWith( "#" ) );

            builder.Attributes( Attributes );
            builder.ElementReferenceCapture( capturedRef => ElementRef = capturedRef );

            if ( Loading && LoadingTemplate != null )
            {
                builder.Content( LoadingTemplate );
            }
            else
            {
                builder.Content( ChildContent );
            }

            builder.CloseElement();
        }

        /// <inheritdoc/>
        protected override RenderFragment ProvideDefaultLoadingTemplate()
        {
            return builder =>
            {
                builder.OpenElement( "span" );
                builder
                    .Class( "spinner-border spinner-border-sm" )
                    .Role( "status" )
                    .AriaHidden( "true" );
                builder.CloseElement();
                builder.Content( ChildContent );
            };
        }

        #endregion
    }
}
